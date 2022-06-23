import * as yup from "yup";

export const schema = yup.object().shape({
	username: yup.string()
		.required()
		.min(3)
		.max(32),
	password: yup.string()
		.required()
		.min(8),
});