import { NextApiRequest, NextApiResponse } from "next";
import * as jwt from "jsonwebtoken";

import { sequelize } from "@components/database";
import { IToken } from "@components/loginForm/models";
import { Scores } from "@models/Scores.model";
import { User } from "@models/User.model";
import { getUserId } from "@core/helpers";

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();
	await Scores.sync();

	const playerId = getUserId(req);
	if (!playerId) {
		res.status(401).end();
		return;
	}

	if (req.method === "GET") {
		const scores = await Scores.findAll({
			attributes: ["creationDate", "time", "vehicle"],
			where: {
				playerId,
			},
		});

		res.status(200).json(scores);
	} else if (req.method === "POST") {
		// Save score
		const { time, vehicle } = req.body;

		try {
			await Scores.create({
				playerId,
				time,
				vehicle,
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
