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
		const user = await User.findOne({where: { username }});
		if (!user) {
			res.status(404).end();
			return;
		}

		res.status(200).end();
	} else {
		res.status(405).end();
	}
};
