import React, { Suspense } from "react";
import { Container } from "reactstrap";
import { AppFooter, AppAside, AppHeader, AppSidebar, AppSidebarFooter, AppSidebarForm, AppSidebarHeader, AppSidebarMinimizer, AppSidebarNav } from "@coreui/react";
// sidebar nav config
import navigation from "./_nav";
// routes config
import routes from "../../../routes";
import Routes from "../../Routes";
import Loading from "../../Loading";

const Aside = React.lazy(() => import("../../Aside/Aside"));
const Footer = React.lazy(() => import("../../Footer/Footer"));
const Header = React.lazy(() => import("../../Header/Header"));

const Layout = ({ ...props }) => (
  <div className="app">
    <AppHeader fixed>
      <Suspense fallback={<Loading.Default />}>
        <Header />
      </Suspense>
    </AppHeader>
    <div className="app-body">
      <AppSidebar fixed minimized display="lg">
        <AppSidebarHeader />
        <AppSidebarForm />
        <Suspense>
          <AppSidebarNav navConfig={navigation} {...props} />
        </Suspense>
        <AppSidebarFooter />
        <AppSidebarMinimizer />
      </AppSidebar>
      <main className="main">
        {/* <AppBreadcrumb appRoutes={routes} /> */}
        <Container fluid>
          <Suspense fallback={<Loading.Default />}>
            <Routes routes={routes} />
          </Suspense>
        </Container>
      </main>
      <AppAside fixed>
        <Suspense fallback={<Loading.Default />}>
          <Aside />
        </Suspense>
      </AppAside>
    </div>
    <AppFooter>
      <Suspense fallback={<Loading.Default />}>
        <Footer />
      </Suspense>
    </AppFooter>
  </div>
);

export default Layout;
