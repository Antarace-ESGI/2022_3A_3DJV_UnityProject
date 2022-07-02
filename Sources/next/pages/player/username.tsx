import Player from "@components/profile/components/Player";
import { useRouter } from "next/router";

export default function Profile() {
	const router = useRouter()
	const { username } = router.query

	return (
		<div className="hero min-h-screen">
			<Player username={ username } />
		</div>
	);
}
