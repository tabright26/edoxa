import React, { Component, Suspense } from "react";
import { Container } from "reactstrap";
import { AppFooter, AppHeader, AppSidebar, AppSidebarFooter, AppSidebarForm, AppSidebarHeader, AppSidebarMinimizer, AppSidebarNav } from "@coreui/react";
// sidebar nav config
import navigation from "../../../_nav";
// routes config
import routes from "../../../routes";
import Routes from "../../Shared/Routes";
import Loading from "../../Shared/Loading";

//const Aside = React.lazy(() => import("./Aside"));
const Footer = React.lazy(() => import("./Footer"));
const Header = React.lazy(() => import("./Header"));

class Layout extends Component {
  render() {
    return (
      <div className="app">
        <AppHeader fixed>
          <Suspense fallback={<Loading />}>
            <Header />
          </Suspense>
        </AppHeader>
        <div className="app-body">
          <AppSidebar fixed display="lg">
            <AppSidebarHeader />
            <AppSidebarForm />
            <Suspense>
              <AppSidebarNav navConfig={navigation} {...this.props} />
            </Suspense>
            <AppSidebarFooter />
            <AppSidebarMinimizer />
          </AppSidebar>
          <main className="main">
            {/* <AppBreadcrumb appRoutes={routes} /> */}
            <Container fluid>
              <Suspense fallback={<Loading />}>
                <Routes routes={routes} />
              </Suspense>
            </Container>
          </main>
          {/* <AppAside fixed>
            <Suspense fallback={<Loading />}>
              <Aside />
            </Suspense>
          </AppAside> */}
        </div>
        <AppFooter>
          <Suspense fallback={<Loading />}>
            <Footer />
          </Suspense>
        </AppFooter>
      </div>
    );
  }
}

export default Layout;
