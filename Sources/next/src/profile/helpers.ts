import { Scores } from "@models/Scores.model";
import { User } from "@models/User.model";

export const getScores = async (username: string, page: number = 0, size: number = 6) => {
	const scores = await Scores.findAll({
		attributes: ["creationDate", "time", "vehicle", "track"],
		include: [{
			model: User,
			required: true,
			where: { username },
			attributes: [],
		}],
		limit: size,
		offset: page * size,
		order: [["creationDate", "DESC"]],
		raw: true,
	});

	return scores;
}