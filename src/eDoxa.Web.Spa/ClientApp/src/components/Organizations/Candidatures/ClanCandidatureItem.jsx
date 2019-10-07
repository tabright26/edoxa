import React, { Fragment, useEffect, useState } from "react";
import { Badge, Col } from "reactstrap";

import CandidatureForm from "forms/Organizations/Candidatures";

const ClanCandidatureItem = ({ candidature, actions, doxaTags, withPermissions }) => {
  const [doxaTag, setDoxaTag] = useState(null);

  useEffect(() => {
    if (doxaTags && candidature) {
      setDoxaTag(doxaTags.find(tag => tag.userId === candidature.userId));
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [doxaTags]);

  return (
    <Fragment>
      <Col xs="4" sm="4" md="4">
        <small className="text-muted">{doxaTag ? doxaTag.name : ""}</small>
      </Col>
      <Col xs="4" sm="4" md="4">
        {withPermissions ? <CandidatureForm.Accept initialValues={{ candidatureId: candidature.id }} onSubmit={data => actions.acceptCandidature(data.candidatureId)} /> : ""}
      </Col>
      <Col xs="4" sm="4" md="4">
        {withPermissions ? <CandidatureForm.Decline initialValues={{ candidatureId: candidature.id }} onSubmit={data => actions.declineCandidature(data.candidatureId)} /> : ""}
      </Col>
    </Fragment>
  );
};

export default ClanCandidatureItem;
