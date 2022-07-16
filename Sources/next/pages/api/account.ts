import { NextApiRequest, NextApiResponse } from "next";
import { createHash } from "crypto";
import * as jwt from "jsonwebtoken";
import { UniqueConstraintError } from "sequelize";

import { User } from "@models/User.model";
import { sequelize } from "@components/database";

async function signup(username: string, password: string): Promise<string> {
	const user = await User.create({
		username,
		password: createHash("sha256").update(password).digest("hex"),
	});

	return jwt.sign(
		{
			id: user.get("id"),
			username: user.get("username"),
		},
		process.env.JWT_SECRET,
		{ algorithm: "HS256" },
	);
}

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();

	const { username, password } = req.body;

	if (req.method === "POST") {
		// Sign up
		try {
			const token = await signup(username, password);
			res.status(201).json({ token });
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
