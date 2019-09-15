import React, { Suspense } from "react";
import { Container } from "reactstrap";
import { AppFooter, AppHeader } from "@coreui/react";
import Loading from "components/Shared/Loading";

const Footer = React.lazy(() => import("components/Shared/Footer"));
const Header = React.lazy(() => import("components/Shared/Header"));

const Layout = ({ children }) => (
  <div className="app">
    <AppHeader fixed>
      <Suspense fallback={<Loading.Default />}>
        <Header />
      </Suspense>
    </AppHeader>
    <div className="app-body">
      <main className="main">
        <Container>
          <Suspense fallback={<Loading.Default />}>{children}</Suspense>
        </Container>
      </main>
    </div>
    <AppFooter>
      <Suspense fallback={<Loading.Default />}>
        <Footer />
      </Suspense>
    </AppFooter>
  </div>
);

export default Layout;
