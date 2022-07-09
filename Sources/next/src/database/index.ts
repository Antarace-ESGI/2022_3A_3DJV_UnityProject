import * as pg from "pg";
import { Sequelize } from "sequelize-typescript";

import { User } from "@models/User.model";
import { Scores } from "@models/Scores.model";

export const sequelize = new Sequelize({
	database: "antarace",
	dialect: "postgres",
	dialectModule: pg,
	username: "root",
	password: "root",
	host: "192.168.2.1",
	models: [User, Scores],
	logging: () => {},
});
