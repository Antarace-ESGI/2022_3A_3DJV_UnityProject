import * as jwt from "jsonwebtoken";
import { IToken } from "@components/loginForm/models";
import { NextApiRequest } from "next";

export function getUserId(req: NextApiRequest): string | null {
	const token = req.headers.authorization.replace("Bearer ", "");
	try {
		jwt.verify(
			token,
			process.env.JWT_SECRET,
		);
	} catch (e) {
		return null;
	}

	const { id } = jwt.decode(token) as IToken;
	return id;
}