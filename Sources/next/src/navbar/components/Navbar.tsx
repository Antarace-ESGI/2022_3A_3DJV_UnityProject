import { useDispatch, useSelector } from "react-redux";
import { RootState } from "@core/store";
import Link from "next/link";
import { useCallback } from "react";
import { setToken } from "@components/loginForm/slice";
import { useRouter } from "next/router";

export default function Navbar() {
	const token = useSelector((state: RootState) => state.token.value);
	const dispatch = useDispatch();
	const router = useRouter()

	const handleLogout = useCallback(() => {
		dispatch(setToken(null));
		router.push("/");
	}, [dispatch]);

	return (
		<div className="navbar bg-base-100">
			<div className="flex-1">
				<Link
					href="/"
					passHref
				>
					<a className="btn btn-ghost normal-case text-xl">Antarace</a>
				</Link>
			</div>
			<div className="flex-none gap-2">
				{
					token && (
						<div className="dropdown dropdown-end">
							<label
								tabIndex={ 0 }
								className="btn btn-ghost btn-circle avatar"
							>
								<div className="w-10 rounded-full">
									<img src="https://placeimg.com/80/80/people" />
								</div>
							</label>
							<ul
								tabIndex={ 0 }
								className="mt-3 p-2 shadow menu menu-compact dropdown-content bg-base-100 rounded-box w-52"
							>
								<li>
									<Link
										href="/profile"
										passHref
									>
										<a className="justify-between">
											Profile
										</a>
									</Link>
								</li>
								<li>
									<a onClick={ handleLogout }>Logout</a>
								</li>
							</ul>
						</div>
					)
				}

				{ !token && (
					<Link
						href="/login"
						passHref
					>
						<a className="btn btn-primary">
							Login
						</a>
					</Link>
				) }
			</div>
		</div>
	);
}