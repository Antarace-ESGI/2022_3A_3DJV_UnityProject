import { NextApiRequest, NextApiResponse } from "next";
import * as jwt from "jsonwebtoken";
import { createHash } from "crypto";

import { User } from "@models/User.model";
import { sequelize } from "@components/database";
import { IToken } from "@components/loginForm/models";
import { Scores } from "@models/Scores.model";

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await Scores.sync();

	const token = req.headers.authorization.replace("Bearer ", "");
	try {
		jwt.verify(
			token,
			process.env.JWT_SECRET,
		);
	} catch (e) {
		res.status(401).end();
	}

	const { id } = jwt.decode(token) as IToken;

	if (req.method === "GET") {
		const scores = await Scores.findAll({
			attributes: ["creationDate", "time"],
			where: {
				playerId: id,
			},
		});

		res.status(200).json(scores);
	} else if (req.method === "POST") {
		// Save score
		const { score } = req.body;

		try {
			await Scores.create({
				playerId: id,
				time: score,
			});

			res.status(201).end();
		} catch (e) {
			console.error(e);
			res.status(500).end();
		}
	} else {
		res.status(405).end();
	}
};
