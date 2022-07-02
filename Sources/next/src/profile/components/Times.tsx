import { useCallback, useEffect, useState } from "react";
import { IScore } from "@components/profile/models";
import { useSelector } from "react-redux";
import { RootState } from "@core/store";
import moment from "moment-timezone";

export default function Times(props) {
	const token = useSelector((state: RootState) => state.token.value);
	const { username } = props;
	const [times, setTimes] = useState<IScore[]>([]);

	useEffect(() => {
		if (!username) return;
		fetch(`/api/player/${ username }/time`)
			.then(res => res.json())
			.then(setTimes);
	}, [username]);

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
							<td>{ duration.humanize() }</td>
							<td>{ moment(creationDate).format("yyyy-MM-DD") }</td>
						</tr>
					)
				}) }
				</tbody>
			</table>
		</div>
	)
}