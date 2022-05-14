import { NextApiRequest } from "next";
import { verify } from "jsonwebtoken";

export function getUserFromRequest(req: NextApiRequest) {
	const token = req.headers.authorization.substring(7);
	const decodedToken = verify(token, process.env.JWT_SECRET, { algorithms: ["HS256"], complete: true });
	const id = decodedToken.payload["id"];
	if (!id) throw new Error("Not authorized");
	return id;
}