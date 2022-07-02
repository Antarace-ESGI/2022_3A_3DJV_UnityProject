import {
	AllowNull, BelongsTo,
	Column,
	CreatedAt,
	DataType,
	Default,
	DeletedAt, ForeignKey,
	Model,
	PrimaryKey,
	Table,
	UpdatedAt,
} from "sequelize-typescript";
import { Moment } from "moment-timezone";
import { User } from "@models/User.model";

@Table({
	timestamps: true,
})
export class Scores extends Model {
	@Default(DataType.UUIDV4)
	@PrimaryKey
	@Column(DataType.UUID)
	declare id: string;

	@ForeignKey(() => User)
	@Column(DataType.UUID)
	declare playerId: string;

	@BelongsTo(() => User, "playerId")
	declare player: User;

	@AllowNull(false)
	@Column(DataType.INTEGER)
	declare time: number;

	@CreatedAt
	declare creationDate: Moment;

	@UpdatedAt
	declare updatedOn: Moment;

	@DeletedAt
	declare deletionDate: Moment;
}