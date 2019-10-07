import React, { Fragment, useEffect, useState } from "react";
import { Col } from "reactstrap";

import CandidatureForm from "forms/Organizations/Candidatures";

const ClanCandidatureItem = ({ candidature, actions, doxaTags, isOwner }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags && candidature) {
      setDoxaTag(doxaTags.find(tag => tag.userId === candidature.userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [doxaTags]);

  return (
    <Fragment>
      <Col>{doxaTag ? doxaTag.name : ""}</Col>
      {isOwner ? (
        <Fragment>
          <Col>
            <CandidatureForm.Accept initialValues={{ candidatureId: candidature.id }} onSubmit={data => actions.acceptCandidature(data.candidatureId)} />
          </Col>
          <Col>
            <CandidatureForm.Decline initialValues={{ candidatureId: candidature.id }} onSubmit={data => actions.declineCandidature(data.candidatureId)} />
          </Col>
        </Fragment>
      ) : (
        ""
      )}
      ;
    </Fragment>
  );
};

export default ClanCandidatureItem;
