import * as pg from "pg";
import { Sequelize } from "sequelize-typescript";

import { User } from "@models/User.model";

export const sequelize = new Sequelize({
	database: "antarace",
	dialect: "postgres",
	dialectModule: pg,
	username: "root",
	password: "root",
	host: "postgres",
	models: [User],
	logging: () => {},
});
