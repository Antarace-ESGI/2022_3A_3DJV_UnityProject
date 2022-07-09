import { useRouter } from "next/router";
import { Scores } from "@models/Scores.model";
import { sequelize } from "@components/database";
import { User } from "@models/User.model";
import Head from "next/head";
import Stats from "@components/profile/components/Stats";
import Times from "@components/profile/components/Times";

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

	const scores = await Scores.findAll({
		attributes: ["creationDate", "time", "vehicle", "track"],
		include: [{
			model: User,
			required: true,
			where: { username },
			attributes: [],
		}],
		order: [["time", "ASC"]],
		raw: true,
	});

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

	return (
		<div className="hero min-h-screen">
			<Head>
				<title>Antarace | { username }&apos;s profile</title>
				<meta name="description" content={ description } />
			</Head>

			<div className="hero-content flex-col w-96">
				<h1 className="text-5xl font-bold">{ username }</h1>
				<hr />

				<Stats
					stats={ stats }
				/>
				<Times
					times={ stats.times }
				/>
			</div>
		</div>
	);
}
