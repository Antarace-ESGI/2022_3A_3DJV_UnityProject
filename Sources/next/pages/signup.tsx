import SignupForm from "@components/loginForm/components/SignupForm";

export default function Home() {
	return (
		<div className="hero min-h-screen">
			<div className="hero-content flex-col w-96">
				<h1 className="text-5xl font-bold">Join us now!</h1>
				<SignupForm />
			</div>
		</div>
	);
}
