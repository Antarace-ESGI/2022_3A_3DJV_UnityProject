import { Formik } from "formik";
import { FormikHelpers } from "formik/dist/types";
import { useCallback } from "react";
import { useDispatch } from "react-redux";

import { IToken, loginSchema } from "@components/loginForm/models";
import { setToken } from "@components/loginForm/slice";
import Link from "next/link";
import { useRouter } from "next/router";
import * as jwt from "jsonwebtoken";

function LoginForm() {
	const dispatch = useDispatch();
	const router = useRouter();

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
				const { username } = jwt.decode(json.token) as IToken ?? {};
				router.push(`/player/${username}`);
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
			validationSchema={ loginSchema }
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
				<form
					noValidate
					onSubmit={ handleSubmit }
					className="card flex-shrink-0 w-full max-w-sm shadow-2xl bg-base-100"
				>
					<div className="card-body">
						<div className="form-control">
							<label className="label">
								<span className="label-text">Username</span>
							</label>
							<input
								type="text"
								placeholder="username"
								name="username"
								className="input input-bordered"
								onChange={ handleChange }
								value={ values.username }
							/>
							<label className="label">
									<span className="label-text-alt">
										{ errors.username as string }
									</span>
							</label>
						</div>
						<div className="form-control">
							<label className="label">
								<span className="label-text">Password</span>
							</label>
							<input
								type="password"
								placeholder="password"
								name="password"
								className="input input-bordered"
								onChange={ handleChange }
								value={ values.password }
							/>
							<label className="label">
									<span className="label-text-alt">
										{ errors.password as string }
									</span>
							</label>
						</div>
						<div className="form-control mt-6">
							<button
								className="btn btn-primary progress-btn"
								type="submit"
								disabled={ isSubmitting }
							>
								Login
							</button>

							<label className="label label-text-alt">
								<Link
									href="/signup"
									passHref
								>
									<a className="link link-hover">
										Don&apos;t have an account yet? Signup!
									</a>
								</Link>
							</label>
						</div>
					</div>
				</form>
			) }
		</Formik>
	);
}

export default LoginForm;