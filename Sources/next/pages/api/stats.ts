import { NextApiRequest, NextApiResponse } from "next";

import { sequelize } from "@components/database";
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
		const stats = await Scores.findOne({
			attributes: [
				[sequelize.fn("count", sequelize.col("id")), "played"],
				[sequelize.fn("sum", sequelize.col("time")), "time"],
			],
			where: {
				playerId,
			},
		});
		const vehicles = await Scores.count({
			attributes: ["vehicle"],
			group: "vehicle",
			where: {
				playerId,
			},
		});

		res.status(200).json({ stats, vehicles });
	} else {
		res.status(405).end();
	}
};
