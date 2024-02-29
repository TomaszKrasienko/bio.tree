pipeline {
    agent any

    environment {
        DOTNET_VERSION = '8.0'
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        stage('Build') {
            steps {
                script {
                    def dotnetCmd = "dotnet build -c Release"
                    bat(dotnetCmd)
                }
            }
        }
    }

    post {
        success {
            echo 'Finished with success'
        }
        failure {
            echo 'Build has errors'
        }
    }
}
