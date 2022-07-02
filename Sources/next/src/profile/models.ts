export interface IScore {
	creationDate: string,
	time: number,
	vehicle: string,
}

export interface IVehicle {
	vehicle: string, // Name of the vehicle
	count: number, // Number of time vehicles was used
}

export interface IStats {
	stats: {
		played: string, // Total games played
		time: string, // Total time played
	},
	vehicles: IVehicle[],
}