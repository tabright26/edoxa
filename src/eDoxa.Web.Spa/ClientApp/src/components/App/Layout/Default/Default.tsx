import React, { Suspense, FunctionComponent } from "react";
import { Container } from "reactstrap";
import {
  AppFooter,
  AppAside,
  AppHeader,
  AppSidebar,
  AppSidebarFooter,
  AppSidebarForm,
  AppSidebarHeader,
  AppSidebarMinimizer,
  AppSidebarNav
} from "@coreui/react";
// sidebar nav config
import navigation from "./_nav";
// routes config
import Routes from "utils/router/components/Routes";
import Loading from "components/Shared/Loading";

const Aside = React.lazy(() => import("components/App/Aside"));
const AppBreadcrumb = React.lazy(() => import("components/App/Breadcrumb"));
const Footer = React.lazy(() => import("components/App/Footer"));
const Header = React.lazy(() => import("components/App/Header"));

const Layout: FunctionComponent<any> = ({ ...props }) => {
  return (
    <div className="app">
      <AppHeader fixed>
        <Suspense fallback={<Loading />}>
          <Header />
        </Suspense>
      </AppHeader>
      <div className="app-body">
        <AppSidebar fixed minimized display="lg">
          <AppSidebarHeader />
          <AppSidebarForm />
          <Suspense fallback={<Loading />}>
            <AppSidebarNav navConfig={navigation} {...props} />
          </Suspense>
          <AppSidebarFooter />
          <AppSidebarMinimizer />
        </AppSidebar>
        <main className="main">
          <AppBreadcrumb />
          <Container fluid>
            <Suspense fallback={<Loading />}>
              <Routes />
            </Suspense>
          </Container>
        </main>
        <AppAside fixed>
          <Suspense fallback={<Loading />}>
            <Aside />
          </Suspense>
        </AppAside>
      </div>
      <AppFooter className="mt-4">
        <Suspense fallback={<Loading />}>
          <Footer />
        </Suspense>
      </AppFooter>
    </div>
  );
};

export default Layout;
