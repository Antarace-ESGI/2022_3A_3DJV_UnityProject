import Stats from "@components/profile/components/Stats";
import Times from "@components/profile/components/Times";

export default function Player(props) {
	const { username } = props;

	return (
		<div className="hero-content flex-col w-96">
			<h1 className="text-5xl font-bold">{ username }</h1>
			<hr />

			<Stats />
			<h2>Scores</h2>
			<Times />
		</div>
	)
}