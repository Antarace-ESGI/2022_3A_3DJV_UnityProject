import { NextApiRequest, NextApiResponse } from "next";
import { sequelize } from "@components/database";
import { User } from "@models/User.model";
import { getScores } from "@components/profile/helpers";
import { Scores } from "@models/Scores.model";

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();
	await Scores.sync();

	if (req.method === "GET") {
		const { username, page: rawPage, size: rawSize } = req.query;
		const page = parseInt(typeof rawPage === "string" ? rawPage : "0");
		const size = parseInt(typeof rawSize === "string" ? rawSize : "6");

		const totalRequest = await Scores.findOne({
			attributes: [
				[sequelize.fn("count", sequelize.col("Scores.id")), "total"],
			],
			include: [{
				model: User,
				required: true,
				where: { username },
				attributes: [],
			}],
			raw: true,
		});

		// @ts-ignore
		const total = parseInt(totalRequest.total);
		const lastPage = total <= (page + 1) * size;

		// @ts-ignore
		const scores = await getScores(username as string, page, size);

		const response = {
			data: scores,
			lastPage,
		}

		res.json(response);
	} else {
		res.status(405).end();
	}
};
