import { NextApiRequest, NextApiResponse } from "next";
import * as jwt from "jsonwebtoken";
import { createHash } from "crypto";

import { User } from "@models/User.model";
import { sequelize } from "@components/database";
import { UniqueConstraintError } from "sequelize";

async function signup(username: string, password: string): Promise<string | null> {
	try {
		await User.create({
			username,
			password: createHash("sha256").update(password).digest("hex"),
		});

		return login(username, password);
	} catch (e) {
		if (e instanceof UniqueConstraintError) {
			return login(username, password);
		} else {
			return null;
		}
	}
}

async function login(username: string, password: string): Promise<string> {
	const user = await User.findOne({
		where: {
			username,
			password: createHash("sha256").update(password).digest("hex"),
		},
	});

	if (user === null) {
		return signup(username, password);
	} else {
		return jwt.sign(
			{
				id: user.get("id"),
				username: user.get("username"),
			},
			process.env.JWT_SECRET,
			{ algorithm: "HS256" },
		);
	}
}

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
	await sequelize.authenticate();
	await User.sync();

	if (req.method === "POST") {
		// Log in
		const { username, password } = req.body;

		const token = await login(username, password);

		res.status(200).json({ token });
	} else {
		res.status(405).end();
	}
};
