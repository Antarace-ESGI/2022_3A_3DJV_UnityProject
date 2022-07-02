import Stats from "@components/profile/components/Stats";
import Times from "@components/profile/components/Times";
import { useEffect, useState } from "react";

export default function Player(props) {
	const { username } = props;
	const [isLoading, setLoading] = useState<boolean>(true);
	const [exists, setExists] = useState<boolean>(false);

	useEffect(() => {
		if (!username) return;
		fetch(`/api/player/${ username }/stats`)
			.then(res => {
				setExists(res.ok);
			})
			.finally(() => {
				setLoading(false);
			});
	}, [username]);


	return (
		<div className="hero-content flex-col w-96">
			<h1 className="text-5xl font-bold">{ username }</h1>
			<hr />

			{ isLoading && (<h1>Loading...</h1>) }
			{ exists && (
				<>
					<Stats username={ username } />
					<Times username={ username } />
				</>
			) }

			{ !exists && !isLoading && (<h1>Player not found</h1>) }
		</div>
	)
}