import { NextApiRequest, NextApiResponse } from "next";
import * as jwt from "jsonwebtoken";
import { createHash } from "crypto";

import { User } from "@models/User.model";
import { sequelize } from "@components/database";

async function login(username: string, password: string): Promise<string | null> {
	const user = await User.findOne({
		where: {
			username,
			password: createHash("sha256").update(password).digest("hex"),
		},
	});

	if (user === null) {
		return null;
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

	const { username, password } = req.body;

	if (req.method === "POST") {
		// Log in
		const token = await login(username, password);

		if (token === null) {
			res.status(400).end();
		} else {
			res.status(200).json({ token });
		}
	} else {
		res.status(405).end();
	}
};
