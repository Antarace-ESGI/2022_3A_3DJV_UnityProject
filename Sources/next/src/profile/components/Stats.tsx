import moment from "moment-timezone";

export default function Stats(props) {
	const { stats } = props;
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