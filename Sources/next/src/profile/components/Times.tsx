import moment from "moment-timezone";

export default function Times(props) {
	const { times } = props;

	return (
		<div className="overflow-x-auto">
			<table className="table w-full">
				<thead>
				<tr>
					<th></th>
					<th>Track</th>
					<th>Vehicle</th>
					<th>Time</th>
					<th>Submit date</th>
				</tr>
				</thead>
				<tbody>
				{ times.map((timeEntry, index) => {
					const { vehicle, time, creationDate,track } = timeEntry;
					const duration = moment.duration(time, "seconds");

					return (
						<tr key={ index }>
							<th>{ index + 1 }</th>
							<td>{ track }</td>
							<td>{ vehicle }</td>
							<td>{ duration.asSeconds() } seconds</td>
							<td>{ moment(creationDate).format("yyyy-MM-DD HH:mm:ss") }</td>
						</tr>
					)
				}) }
				</tbody>
			</table>
		</div>
	)
}