import Link from "next/link";

export default function Navbar() {
	return (
		<header className="container d-flex flex-wrap align-items-center justify-content-center justify-content-lg-between py-3 mb-4 border-bottom">
			<Link
				href="/"
				passHref
			>
				<a className="d-flex align-items-center col-lg-3 mb-2 mb-lg-0 text-dark text-decoration-none">
					Home
				</a>
			</Link>

			<div className="col-lg-3 text-end text-nowrap">
				<Link
					href="/login"
					passHref
				>
					<a className="btn btn-primary">
						Login
					</a>
				</Link>
			</div>
		</header>
	);
}