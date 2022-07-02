import { NextApiRequest, NextApiResponse } from "next";
import { createHash } from "crypto";

import { User } from "@models/User.model";
import { sequelize } from "@components/database";
import { UniqueConstraintError } from "sequelize";

async function signup(username: string, password: string): Promise<void> {
	await User.create({
		username,
		password: createHash("sha256").update(password).digest("hex"),
	});
}

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();

	const { username, password } = req.body;

	if (req.method === "POST") {
		// Sign up
		try {
			await signup(username, password);
			res.status(201).end();
		} catch (e) {
			if (e instanceof UniqueConstraintError) {
				res.status(409).end();
			} else {
				res.status(400).end();
			}
		}
	} else {
		res.status(405).end();
	}
};
