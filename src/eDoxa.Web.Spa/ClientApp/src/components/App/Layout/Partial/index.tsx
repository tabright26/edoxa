import React, { Suspense } from "react";
import { Container } from "reactstrap";
import { AppFooter, AppHeader } from "@coreui/react";
import { Loading } from "components/Shared/Loading";

const Footer = React.lazy(() => import("components/App/Footer"));
const Header = React.lazy(() => import("components/App/Header"));

export const Partial = ({ children }) => (
  <div className="app">
    <AppHeader fixed>
      <Suspense fallback={<Loading />}>
        <Header />
      </Suspense>
    </AppHeader>
    <div className="app-body">
      <main className="main">
        <Container>
          <Suspense fallback={<Loading />}>{children}</Suspense>
        </Container>
      </main>
    </div>
    <AppFooter>
      <Suspense fallback={<Loading />}>
        <Footer />
      </Suspense>
    </AppFooter>
  </div>
);

export default Partial;
