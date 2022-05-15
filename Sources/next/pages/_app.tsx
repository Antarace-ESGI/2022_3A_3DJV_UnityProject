import { AppProps } from "next/app";
import { Provider } from "react-redux";
import { PersistGate } from "redux-persist/integration/react";

import { persistor, store } from "@core/store";
import Navbar from "@components/navbar/components/Navbar";

import "bootstrap/dist/css/bootstrap.min.css";

function App({ Component, pageProps }: AppProps) {
  return (
      <Provider store={ store }>
        <PersistGate
            loading={ null }
            persistor={ persistor }
        >
          <Navbar />
          <main className="container">
            <Component { ...pageProps } />
          </main>
          <footer className="container" />
        </PersistGate>
      </Provider>
  );
}

export default App;