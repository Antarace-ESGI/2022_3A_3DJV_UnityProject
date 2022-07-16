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
import * as jwt from "jsonwebtoken";
import { IToken } from "@components/loginForm/models";
import { useSelector } from "react-redux";
import { RootState } from "@core/store";
import Link from "next/link";

export async function getServerSideProps(context) {
	const { res, query } = context;
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

	if (!stats) {
		res.statusCode = 404;
		return {
			props: {
				stats: null,
			},
		}
	}

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

	const token = useSelector((state: RootState) => state.token.value);
	const { username: selfUsername } = jwt.decode(token) as IToken ?? {};

	const description = stats
		? `${ stats.played } played games for a total of ${ stats.time } seconds.`
		: "Player not found.";

	const [times, setTimes] = useState<IScore[]>(stats?.times ?? []);
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

				{ stats && (
					<>
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
					</>
				) }

				{ !stats && selfUsername !== username && (
					"Player not found."
				) }

				{ !stats && selfUsername === username && (
					<>
						Download the game and register your first time now!.
						<Link
							href="https://github.com/Quozul/2022_3A_3DJV_UnityProject/releases"
							passHref
						>
							<a
								className="btn btn-primary"
								target="_blank"
								rel="noreferrer noopener"
							>
								Download
							</a>
						</Link>
					</>
				) }
			</div>
		</div>
	);
}
