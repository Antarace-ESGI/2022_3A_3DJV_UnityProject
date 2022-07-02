import { useSelector } from "react-redux";
import { RootState } from "@core/store";
import { useCallback, useEffect, useState } from "react";
import { IStats } from "@components/profile/models";
import moment from "moment-timezone";

export default function Stats() {
	const token = useSelector((state: RootState) => state.token.value);
	const [stats, setStats] = useState<IStats>(null);

	const fetchStats = useCallback(async () => {
		const response = await fetch("/api/stats", {
			headers: {
				authorization: `Bearer ${ token }`,
			}
		});
		setStats(await response.json());
	}, [token]);

	useEffect(() => {
		fetchStats();
	}, [fetchStats]);

	const duration = moment.duration(stats?.stats?.time || 0, "seconds");
	const formattedDuration = `${duration.minutes()}:${duration.seconds()}`

	return (
		<div className="stats shadow">
			<div className="stat">
				<div className="stat-title">Most used vehicle</div>
				<div className="stat-value">Agil</div>
			</div>
			<div className="stat">
				<div className="stat-title">Total time played</div>
				<div className="stat-value">{ formattedDuration }</div>
			</div>
			<div className="stat">
				<div className="stat-title">Games played</div>
				<div className="stat-value">{ stats?.stats?.played }</div>
			</div>
		</div>
	)
}