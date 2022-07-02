import { useSelector } from "react-redux";
import { RootState } from "@core/store";
import { useEffect, useState } from "react";
import { IStats } from "@components/profile/models";
import moment from "moment-timezone";

export default function Stats(props) {
	const token = useSelector((state: RootState) => state.token.value);
	const { username } = props;
	const [stats, setStats] = useState<IStats>(null);

	useEffect(() => {
		if (!username) return;
		fetch(`/api/player/${ username }/stats`)
			.then(res => res.json())
			.then(setStats);
	}, [setStats, token, username]);

	const duration = moment.duration(stats?.time || 0, "seconds");

	return (
		<div className="stats shadow">
			<div className="stat">
				<div className="stat-title">Most used vehicle</div>
				<div className="stat-value">{ stats?.vehicle.name }</div>
			</div>
			<div className="stat">
				<div className="stat-title">Total time played</div>
				<div className="stat-value">{ duration.humanize() }</div>
			</div>
			<div className="stat">
				<div className="stat-title">Races played</div>
				<div className="stat-value">{ stats?.played || 0 }</div>
			</div>
		</div>
	)
}