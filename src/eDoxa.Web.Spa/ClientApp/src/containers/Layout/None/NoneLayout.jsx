import React, { Suspense } from "react";
import { Container } from "reactstrap";
// routes config
import routes from "../../Routing/Routes/security/routes";
import RouteHandler from "../../Routing/RouteHandler";
import Loading from "../../../components/Loading";

const Layout = ({ ...props }) => (
  <div className="app">
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
  </div>
);

export default Layout;
