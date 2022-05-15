import {
	AllowNull,
	Column,
	CreatedAt,
	DataType,
	Default,
	DeletedAt,
	Model,
	PrimaryKey,
	Table,
	Unique,
	UpdatedAt,
} from "sequelize-typescript";
import { Moment } from "moment-timezone";

@Table({
	timestamps: true,
})
export class User extends Model {
	@Default(DataType.UUIDV4)
	@PrimaryKey
	@Column(DataType.UUID)
	declare id: string;

	@Unique
	@AllowNull(false)
	@Column(DataType.STRING(32))
	declare username: string;

	@AllowNull(false)
	@Column(DataType.STRING(64))
	declare password: string;

	@CreatedAt
	declare creationDate: Moment;

	@UpdatedAt
	declare updatedOn: Moment;

	@DeletedAt
	declare deletionDate: Moment;
}