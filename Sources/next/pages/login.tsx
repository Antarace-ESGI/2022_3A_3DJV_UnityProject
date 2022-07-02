import LoginForm from "@components/loginForm/components/LoginForm";

export default function Home() {
	return (
		<div className="hero min-h-screen">
			<div className="hero-content flex-col w-96">
				<h1 className="text-5xl font-bold">Login now!</h1>
				<LoginForm />
			</div>
		</div>
	);
}
