import { useRouter } from "next/router";
import { Scores } from "@models/Scores.model";
import { sequelize } from "@components/database";
import { User } from "@models/User.model";
import Head from "next/head";
import Stats from "@components/profile/components/Stats";
import Times from "@components/profile/components/Times";
import { getScores } from "@components/profile/helpers";
import { useCallback, useState } from "react";
import { IScore } from "@components/profile/models";

export async function getServerSideProps({ query }) {
	const { username } = query;

	await sequelize.authenticate();

	const stats = await Scores.findOne({
		attributes: [
			[sequelize.fn("count", sequelize.col("Scores.id")), "played"],
			[sequelize.fn("sum", sequelize.col("time")), "time"],
		],
		include: [{
			model: User,
			required: true,
			where: { username },
			attributes: [],
		}],
		group: "player.id",
		raw: true,
	});

	const vehicle = await Scores.findOne({
		attributes: [
			["vehicle", "name"],
			[sequelize.fn("count", sequelize.col("Scores.id")), "used"],
		],
		include: [{
			model: User,
			required: true,
			where: { username },
			attributes: [],
		}],
		group: ["vehicle"],
		order: [[sequelize.col("used"), "DESC"]],
		raw: true,
	});

	const scores = await getScores(username);

	const data = stats ? {
		...stats,
		vehicle: vehicle,
		times: JSON.parse(JSON.stringify(scores)),
	} : null;

	return {
		props: {
			stats: data,
		},
	}
}

export default function Profile(props) {
	const router = useRouter()
	const { username } = router.query
	const { stats } = props;
	const description = `${ stats.played } played games for a total of ${ stats.time } seconds.`;

	const [times, setTimes] = useState<IScore[]>(stats.times);
	const [page, setPage] = useState<number>(0);
	const [lastPage, setLastPage] = useState<boolean>(false);

	const handleLoadMore = useCallback(() => {
		const url = new URL(`/api/player/${ username }/score`, window.location.origin);
		url.searchParams.append("page", (page + 1).toString());

		fetch(url.href)
			.then(res => res.json())
			.then(json => {
				const { data, lastPage } = json;

				setTimes(prevState => [...prevState, ...data]);
				setPage(page + 1);
				setLastPage(lastPage);
			});
	}, [times, setTimes, page, setPage, setLastPage]);


	return (
		<div className="hero min-h-screen">
			<Head>
				<title>Antarace | { username }&apos;s profile</title>
				<meta
					name="description"
					content={ description }
				/>
			</Head>

			<div className="hero-content flex-col w-96">
				<h1 className="text-5xl font-bold">{ username }</h1>
				<hr />

				<Stats
					stats={ stats }
				/>
				<Times
					times={ times }
				/>

				<button
					className="btn btn-primary"
					onClick={ handleLoadMore }
					disabled={ lastPage }
				>
					Load more
				</button>
			</div>
		</div>
	);
}
