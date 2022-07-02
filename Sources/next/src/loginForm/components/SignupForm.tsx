import { Formik } from "formik";
import { FormikHelpers } from "formik/dist/types";
import { useCallback } from "react";
import { useDispatch } from "react-redux";

import { signupSchema } from "@components/loginForm/models";
import { setToken } from "@components/loginForm/slice";
import Link from "next/link";
import { useRouter } from "next/router";

function LoginForm() {
	const dispatch = useDispatch();
	const router = useRouter()

	const handleSubmit = useCallback((values: any, helpers: FormikHelpers<any>) => {
		const { username, password, confirmPassword } = values;
		const { setSubmitting, setErrors } = helpers;

		if (password !== confirmPassword) {
			setErrors({
				password: "password must match.",
			});
			return;
		}

		fetch("/api/account", {
			method: "post",
			headers: {
				"Content-Type": "application/json",
			},
			body: JSON.stringify({ username, password }),
		})
			.then((res) => res.json())
			.then((json) => {
				dispatch(setToken(json.token));
				router.push("/login");
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
			validationSchema={ signupSchema }
			onSubmit={ handleSubmit }
			initialValues={ {
				username: "",
				password: "",
				confirmPassword: "",
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

						<div className="form-control">
							<label className="label">
								<span className="label-text">Confirm password</span>
							</label>
							<input
								type="password"
								placeholder="confirm password"
								name="confirmPassword"
								className="input input-bordered"
								onChange={ handleChange }
								value={ values.confirmPassword }
							/>
							<label className="label">
								<span className="label-text-alt">
									{ errors.confirmPassword as string }
								</span>
							</label>
						</div>

						<div className="form-control mt-6">
							<button
								className="btn btn-primary progress-btn"
								type="submit"
								disabled={ isSubmitting }
							>
								Signup
							</button>

							<label className="label label-text-alt">
								<Link
									href="/login"
									passHref
								>
									<a className="link link-hover">
										Already have an account? Login!
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