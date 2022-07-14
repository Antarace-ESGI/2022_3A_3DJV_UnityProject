import { ThemeProvider } from "next-themes";
import { AppProps } from "next/app";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";

import { persistor, store } from "@core/store";
import Navbar from "@components/navbar/components/Navbar";
import Footer from "@components/navbar/components/Footer";

import "../styles/globals.css";

function App({ Component, pageProps }: AppProps) {
	return (
		<ThemeProvider defaultTheme="night">
			<Provider store={ store }>
				<PersistGate
					loading={ null }
					persistor={ persistor }
				>
					<Navbar />
					<main>
						<Component { ...pageProps } />
					</main>
					<Footer />
				</PersistGate>
			</Provider>
		</ThemeProvider>
	);
}

export default App;