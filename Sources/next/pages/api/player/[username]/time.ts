import { NextApiRequest, NextApiResponse } from "next";

import { sequelize } from "@components/database";
import { Scores } from "@models/Scores.model";
import { User } from "@models/User.model";

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();
	await Scores.sync();

	const { username } = req.query;

	if (req.method === "GET") {
		const scores = await Scores.findAll({
			attributes: ["creationDate", "time", "vehicle", "track"],
			include: [{
				model: User,
				required: true,
				where: { username },
				attributes: [],
			}],
			order: [["time", "ASC"]],
		});

		res.status(200).json(scores);
	} else {
		res.status(405).end();
	}
};
