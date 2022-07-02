import { useSelector } from "react-redux";
import * as jwt from "jsonwebtoken";
import { RootState } from "@core/store";
import { IToken } from "@components/loginForm/models";
import Stats from "@components/profile/components/Stats";
import Times from "@components/profile/components/Times";

export default function Profile() {
	const token = useSelector((state: RootState) => state.token.value);
	const { username } = jwt.decode(token) as IToken;

	return (
		<div className="hero min-h-screen">
			<div className="hero-content flex-col w-96">
				<h1 className="text-5xl font-bold">{ username }</h1>
				<hr />

				<Stats />

				<h2>Scores</h2>

				<Times />
			</div>
		</div>
	);
}
