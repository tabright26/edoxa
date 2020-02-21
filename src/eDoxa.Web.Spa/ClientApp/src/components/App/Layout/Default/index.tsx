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
import navConfig from "utils/coreui/_nav";
// routes config
import Routes from "utils/router/components/Routes";
import { Loading } from "components/Shared/Loading";
import { Social } from "components/App/Footer/Social";

const Aside = React.lazy(() => import("components/App/Aside"));
const Footer = React.lazy(() => import("components/App/Footer"));
const Header = React.lazy(() => import("components/App/Header"));

export const Default: FunctionComponent = () => {
  return (
    <div className="app" style={{ marginTop: -1 }}>
      <AppHeader fixed>
        <Suspense fallback={<Loading />}>
          <Header />
        </Suspense>
      </AppHeader>
      <div className="app-body">
        <AppSidebar fixed minimized={false} display="md">
          <AppSidebarHeader />
          <AppSidebarForm />
          <Suspense fallback={<Loading />}>
            <AppSidebarNav navConfig={navConfig} />
          </Suspense>
          <AppSidebarFooter />
          <AppSidebarMinimizer />
        </AppSidebar>
        <main className="main">
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
          <Social className="mr-auto" />
          <Footer className="ml-auto" />
        </Suspense>
      </AppFooter>
    </div>
  );
};

export default Default;
