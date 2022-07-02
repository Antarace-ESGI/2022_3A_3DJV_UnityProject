export interface IScore {
	creationDate: string,
	time: number,
	vehicle: string,
	track: string,
}

export interface IStats {
	played: string, // Total games played
	time: string, // Total time played
	vehicle: {
		name: string, // The name of the vehicle
		used: number, // Times used
	},
}