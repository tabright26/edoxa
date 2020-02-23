import React, { FunctionComponent } from "react";
import { Alert, Container, Badge } from "reactstrap";
import { REACT_APP_BETA_ALERT_DISPLAYED } from "keys";
import { Discord } from "components/Shared/Social";

export const Banner: FunctionComponent = () =>
  REACT_APP_BETA_ALERT_DISPLAYED === "true" ? (
    <Alert className="mb-0 rounded-0 w-100 bg-primary text-light border-0">
      <Container className="text-justify">
        Dear new eDoxa users, we welcome you to the Beta version of our eSport
        platform! The team wants to help unlock every gamer’s potential so that
        they can be proud to live their eSport dream. Please feel free to
        contact us with our live chat below or our{" "}
        <Discord>
          <Badge
            color="dark"
            style={{
              cursor: "pointer"
            }}
          >
            Discord
          </Badge>
        </Discord>
        server to provide feedback and suggestions about eDoxa.gg.
      </Container>
    </Alert>
  ) : null;
