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
	host: process.env.DATABASE_HOST ?? "localhost",
	models: [User, Scores],
	logging: () => {},
});
