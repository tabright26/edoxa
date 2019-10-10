import React, { Fragment } from "react";
import { toastr } from "react-redux-toastr";
import { LinkContainer } from "react-router-bootstrap";
import { Col, Button } from "reactstrap";

const ClanCandidatureItem = ({ candidature, actions, type, isOwner }) => {
  return (
    <Fragment>
      <Col>
        {type === "clan" ? (
          candidature.doxaTag
        ) : (
          <Col>
            <LinkContainer to={"/structures/clans/" + candidature.clanId}>
              <Button color="primary">{candidature.clanName}</Button>
            </LinkContainer>
          </Col>
        )}
      </Col>
      {isOwner && type === "clan" ? (
        <Fragment>
          <Button
            color="info"
            onClick={() =>
              actions
                .acceptCandidature(candidature.id)
                .then(toastr.success("SUCCESS", "Candidature was accepted."))
                .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Candidature not was accepted."))
            }
          >
            Accept candidature
          </Button>
          <Button
            color="danger"
            onClick={() =>
              actions
                .declineCandidature(candidature.id)
                .then(toastr.success("SUCCESS", "Candidature was decline."))
                .catch(toastr.error("WARNINGAVERTISSEMENTAVECLELOGODUFBIQUIDECOLEPUAVANTLEFILM", "Candidature not was accepted."))
            }
          >
            Decline candidature
          </Button>
        </Fragment>
      ) : null}
    </Fragment>
  );
};

export default ClanCandidatureItem;
