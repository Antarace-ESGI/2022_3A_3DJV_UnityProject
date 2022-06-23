import { Formik } from "formik";
import { Button, Col, Form, InputGroup, Spinner } from "react-bootstrap";
import { FormikHelpers } from "formik/dist/types";
import { useCallback } from "react";
import { useDispatch, useSelector } from "react-redux";

import { schema } from "@components/loginForm/models";
import { setToken } from "@components/loginForm/slice";
import { RootState } from "@core/store";

function LoginForm() {
	const dispatch = useDispatch();
	const token = useSelector((state: RootState) => state.token.value);

	const handleSubmit = useCallback((values: any, helpers: FormikHelpers<any>) => {
		const { username, password } = values;
		const { setSubmitting, setErrors } = helpers;

		fetch("/api/session", {
			method: "post",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({ username, password }),
		})
			.then((res) => res.json())
			.then((json) => {
				dispatch(setToken(json.token));
			})
			.catch((err) => {
				setErrors(err);
			})
			.finally(() => {
				setSubmitting(false);
			});
	}, [dispatch]);

	return (
		<Formik
			validationSchema={ schema }
			onSubmit={ handleSubmit }
			initialValues={ {
				username: "",
				password: "",
			} }
		>
			{ ({
				   handleSubmit,
				   handleChange,
				   values,
				   isSubmitting,
				   errors,
			   }) => (
				<Form
					noValidate
					onSubmit={ handleSubmit }
				>
					<h2>Login</h2>
					<Form.Group
						as={ Col }
						md="4"
					>
						<Form.Label>Pseudonyme</Form.Label>
						<InputGroup hasValidation>
							<Form.Control
								type="text"
								placeholder="Pseudonyme"
								name="username"
								value={ values.email }
								onChange={ handleChange }
								isInvalid={ !!errors.email }
							/>
							<Form.Control.Feedback type="invalid">
								{ errors.username as string }
							</Form.Control.Feedback>
						</InputGroup>
					</Form.Group>

					<Form.Group
						as={ Col }
						md="4"
					>
						<Form.Label>Password</Form.Label>
						<InputGroup hasValidation>
							<Form.Control
								type="password"
								placeholder="Password"
								name="password"
								value={ values.password }
								onChange={ handleChange }
								isInvalid={ !!errors.password }
							/>
							<Form.Control.Feedback type="invalid">
								{ errors.password as string }
							</Form.Control.Feedback>
							<Form.Control.Feedback type="valid">
								Seems good
							</Form.Control.Feedback>
						</InputGroup>
					</Form.Group>

					<Button type="submit">
						{ isSubmitting && <Spinner size="sm" animation="border" /> }
						Login
					</Button>

					<div>
						{ token ?? "null" }
					</div>
				</Form>
			) }
		</Formik>
	);
}

export default LoginForm;