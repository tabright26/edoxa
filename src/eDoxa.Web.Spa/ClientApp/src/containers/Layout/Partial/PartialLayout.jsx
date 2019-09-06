import React, { Suspense } from "react";
import { Container } from "reactstrap";
import { AppFooter, AppHeader } from "@coreui/react";
// routes config
import routes from "../../Routing/Routes/information/routes";
import RouteHandler from "../../Routing/RouteHandler";
import Loading from "../../../components/Loading";

const Footer = React.lazy(() => import("../../../components/Shared/Footer"));
const Header = React.lazy(() => import("../../../components/Shared/Header"));

const Layout = ({ ...props }) => (
  <div className="app">
    <AppHeader fixed>
      <Suspense fallback={<Loading.Default />}>
        <Header />
      </Suspense>
    </AppHeader>
    <div className="app-body">
      <main className="main">
        {/* <AppBreadcrumb appRoutes={routes} /> */}
        <Container fluid>
          <Suspense fallback={<Loading.Default />}>
            <RouteHandler routes={routes} />
          </Suspense>
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
