import { useCallback, useEffect, useState } from "react";
import { IScore } from "@components/profile/models";
import { useSelector } from "react-redux";
import { RootState } from "@core/store";
import moment from "moment-timezone";

export default function Times() {
	const token = useSelector((state: RootState) => state.token.value);
	const [times, setTimes] = useState<IScore[]>([]);

	const fetchTimes = useCallback(async () => {
		const response = await fetch("/api/time", {
			headers: {
				authorization: `Bearer ${ token }`,
			}
		});
		setTimes(await response.json());
	}, [token]);

	useEffect(() => {
		fetchTimes();
	}, [fetchTimes]);

	return (
		<div className="overflow-x-auto">
			<table className="table w-full">
				<thead>
				<tr>
					<th></th>
					<th>Vehicle</th>
					<th>Time</th>
					<th>Submit date</th>
				</tr>
				</thead>
				<tbody>
				{ times.map((timeEntry, index) => {
					const {vehicle, time, creationDate} = timeEntry;
					const duration = moment.duration(time, "seconds");
					const formattedDuration = `${duration.minutes()}:${duration.seconds()}`

					return (
						<tr key={index}>
							<th>{ index + 1 }</th>
							<td>{ vehicle }</td>
							<td>{ formattedDuration }</td>
							<td>{ moment(creationDate).format("yyyy-MM-DD") }</td>
						</tr>
					)
				}) }
				</tbody>
			</table>
		</div>
	)
}