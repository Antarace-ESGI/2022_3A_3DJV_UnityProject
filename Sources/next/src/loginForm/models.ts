import * as yup from "yup";

export const loginSchema = yup.object().shape({
	username: yup.string()
		.required()
		.min(3)
		.max(32),
	password: yup.string()
		.required()
		.min(8),
});

export const signupSchema = yup.object().shape({
	username: yup.string()
		.required()
		.min(3)
		.max(32),
	password: yup.string()
		.required()
		.min(8),
	confirmPassword: yup.string()
		.required()
		.oneOf([yup.ref("password"), null], "passwords must match"),
});

export interface IToken {
	id: string,
	username: string,
	iat: number,
}