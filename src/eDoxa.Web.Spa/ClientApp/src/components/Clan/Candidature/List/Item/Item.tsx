import React, { Fragment } from "react";
import { toastr } from "react-redux-toastr";
import { LinkContainer } from "react-router-bootstrap";
import { Button } from "reactstrap";

const Item = ({ candidature, actions, type, isOwner }) => {
  return (
    <Fragment>
      {type === "clan" ? (
        candidature.doxatag
      ) : (
        <LinkContainer to={"/structures/clans/" + candidature.clanId}>
          <Button className="d-block" color="primary">
            {candidature.clan ? candidature.clan.name : "NOT LOADED"}
          </Button>
        </LinkContainer>
      )}
      {isOwner && type === "clan" && (
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
      )}
    </Fragment>
  );
};

export default Item;
