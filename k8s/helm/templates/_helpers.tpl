{{/* vim: set filetype=mustache: */}}
{{/*
Format internal health checks url.
*/}}
{{- define "url.readiness" -}}
{{- $serviceName := .ServiceName -}}
{{- $context := .Context -}}
{{- $serviceUrl := (include "url.internal" (dict "ServiceName" $serviceName "Context" $context)) -}}
{{- printf "%s/health" $serviceUrl -}}
{{- end -}}

{{/*
Format internal url.
*/}}
{{- define "url.internal" -}}
{{- $serviceName := .ServiceName -}}
{{- $context := .Context -}}
{{- printf "http://%s-%s" $context.Release.Name $serviceName -}}
{{- end -}}

{{/*
Format external url form ingress path.
*/}}
{{- define "url.external" -}}
{{- $ingressPath := .IngressPath -}}
{{- $context := .Context -}}
{{- printf "http://%s/%s" $context.Values.global.app.host $ingressPath -}}
{{- end -}}

{{/*
SQL Server connection string
*/}}
{{- define "mssql" -}}
{{- printf "Server=%s-%s;Database=%s;User Id=%s;Password=%s;" .Release.Name .Values.global.infrastructure.mssql.host .Values.infrastructure.mssql.database.name "sa" "RBCLVNRS8HPdksJj8bax" -}}
{{- end -}}

{{/*
Redis connection string
*/}}
{{- define "redis" -}}
{{- printf "http://%s-%s" .Release.Name .Values.global.infrastructure.redis.host -}}
{{- end -}}

{{/*
RabbitMQ host name
*/}}
{{- define "rabbitmq" -}}
{{- printf "http://%s-%s" .Release.Name .Values.global.infrastructure.rabbitmq.host -}}
{{- end -}}

{{/*
Serilog Sink Seq url
*/}}
{{- define "seq" -}}
{{- printf "http://%s-%s" .Release.Name .Values.global.infrastructure.seq.host -}}
{{- end -}}

{{/*
Namespace configmap fullname
*/}}
{{- define "subchart.configmap.fullname" -}}
{{- $fullname := include "subchart.fullname" . -}}
{{- printf "%s-%s" $fullname "configmap" -}}
{{- end -}}

{{/*
Namespace service fullname
*/}}
{{- define "subchart.service.fullname" -}}
{{- $fullname := include "subchart.fullname" . -}}
{{- printf "%s-%s" $fullname "service" -}}
{{- end -}}

{{/*
Expand the name of the chart.
*/}}
{{- define "subchart.name" -}}
{{- default .Chart.Name .Values.nameOverride | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Create a default fully qualified app name.
We truncate at 63 chars because some Kubernetes name fields are limited to this (by the DNS naming spec).
If release name contains chart name it will be used as a full name.
*/}}
{{- define "subchart.fullname" -}}
{{- if .Values.fullnameOverride -}}
{{- .Values.fullnameOverride | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- $name := default .Chart.Name .Values.nameOverride -}}
{{- if contains $name .Release.Name -}}
{{- .Release.Name | trunc 63 | trimSuffix "-" -}}
{{- else -}}
{{- printf "%s-%s" .Release.Name $name | trunc 63 | trimSuffix "-" -}}
{{- end -}}
{{- end -}}
{{- end -}}

{{/*
Create chart name and version as used by the chart label.
*/}}
{{- define "subchart.chart" -}}
{{- printf "%s-%s" .Chart.Name .Chart.Version | replace "+" "_" | trunc 63 | trimSuffix "-" -}}
{{- end -}}

{{/*
Common labels
*/}}
{{- define "subchart.labels" -}}
app.kubernetes.io/name: {{ include "subchart.name" . }}
helm.sh/chart: {{ include "subchart.chart" . }}
app.kubernetes.io/instance: {{ .Release.Name }}
{{- if .Chart.AppVersion }}
app.kubernetes.io/version: {{ .Chart.AppVersion | quote }}
{{- end }}
app.kubernetes.io/managed-by: {{ .Release.Service }}
{{- end -}}