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
		const stats = await Scores.findOne({
			attributes: [
				[sequelize.fn("count", sequelize.col("Scores.id")), "played"],
				[sequelize.fn("sum", sequelize.col("time")), "time"],
			],
			include: [{
				model: User,
				required: true,
				where: { username },
				attributes: [],
			}],
			group: "player.id",
		});

		const vehicle = await Scores.findOne({
			attributes: [
				["vehicle", "name"],
				[sequelize.fn("count", sequelize.col("Scores.id")), "used"],
			],
			include: [{
				model: User,
				required: true,
				where: { username },
				attributes: [],
			}],
			group: ["vehicle"],
			order: [[sequelize.col("used"), "DESC"]],
		});

		if (stats && vehicle) {
			res.status(200).json({ ...stats.toJSON(), vehicle });
		} else {
			res.status(404).end();
		}
	} else {
		res.status(405).end();
	}
};
